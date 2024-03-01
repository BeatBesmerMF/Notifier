# syntax=docker/dockerfile:1-labs

FROM metacall/guix:latest AS GUIX
RUN --security=insecure \
    source $GUIX_PROFILE/etc/profile && \
    sh -c "/root/.config/guix/current/bin/guix-daemon --build-users-group=guixbuild &" && \
    sleep 5 && \
    guix pull && \
    mkdir -p /tmp/tools && \
    cp "$(guix pack -f tarball -C none -S /usr/local/bin=bin \
    node clojure clojure-tools openjdk:jdk \
    )" tools.tar

FROM lscr.io/linuxserver/code-server:latest
RUN --mount=type=bind,from=GUIX,source=/tmp/tools,target=/tmp/tools \
    mkdir -p /tmp/tools/extracted && \
    cd /tmp/tools/extracted && \
    tar xf ../tools.tar && \
    cp -r ./gnu / && \
    cd tools/bin && \
    find ./ ! -type d -exec cp -a {} /bin/{} \; && \
    cd /usr/bin && \
    curl https://raw.githubusercontent.com/technomancy/leiningen/stable/bin/lein > lein && \
    chmod +x /usr/bin/lein && \
    cd / && rm -rf /tmp/tools/extracted
