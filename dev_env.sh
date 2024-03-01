#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR=$(realpath "$(dirname "${BASH_SOURCE[0]}")")
CONFIG=${SCRIPT_DIR}/.code-server-config
WORKSPACE=${SCRIPT_DIR}

SRCS=("${SCRIPT_DIR}/$(basename "${BASH_SOURCE[0]}")"
      "${SCRIPT_DIR}/docker/Dockerfile"
      "${SCRIPT_DIR}/docker/DockerfileGuix")

HASH=$(cat "${SRCS[@]}" | sha256sum)
HASH=${HASH:0:16}

if [[ -z "$(docker images -q "clj-code:${HASH}")" ]]; then
    if (guix --version &>/dev/null); then
        tools=$(guix pack -f tarball \
		     -C none -S /tools/bin=bin \
		     node clojure clojure-tools openjdk:jdk)
	cp -f "${tools}" "${SCRIPT_DIR}/docker/tools.tar"
        DOCKER_BUILDKIT=1 docker build \
                          -t "clj-code:${HASH}" \
                          -f "${SCRIPT_DIR}/docker/DockerfileGuix" "${SCRIPT_DIR}/docker"
    else
        docker buildx create \
               --buildkitd-flags '--allow-insecure-entitlement security.insecure' \
               --name insecure-builder || :
        docker buildx use insecure-builder
        docker buildx build --load --allow security.insecure \
               -t "clj-code:${HASH}" \
               -f "${SCRIPT_DIR}/docker/Dockerfile" "${SCRIPT_DIR}/docker"
    fi;
fi;

docker run --rm -d \
       --name "clj-code-${HASH}" \
       -e PUID="$(id -u)" \
       -e PGID="$(id -g)" \
       -e TZ=Etc/UTC \
       -e DEFAULT_WORKSPACE=/workspace \
       -v "${CONFIG}:/config" \
       -v "${WORKSPACE}:/workspace" \
       "clj-code:${HASH}" || :

DOCKER_IP=$(docker inspect -f \
		   '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' \
		   "clj-code-${HASH}")

sleep 2;
xdg-open "http://${DOCKER_IP}:8443"
