(ns notifier-clj.commands.subscriptions
  (:require [compojure.core :refer [POST]]
            [clojure.data.json :refer [json-str]]
            [clojure.spec.alpha :as spec]
            [notifier-clj.schemas.events :as e]
            [notifier-clj.events.event-store :as event-store]))

(spec/def ::userId string?)
(spec/def ::machineId string?)
(spec/def ::subscription-detail (spec/keys :req-un [::userId ::machineId]))

(defn- subscribe-to-machine [req]
  (if (spec/valid? ::subscription-detail (:body req))
    (let [subscription-id (random-uuid)
          event (e/make-event
                 ::e/user-subscribed-to-machine
                 (format "/subscriptions/%s" subscription-id)
                 {:UserId (-> req :body :userId)
                  :MachineId (-> req :body :machineId)})]
      (event-store/store-events [event])
      {:status 204})
    {:status 400
     :headers {"Content-Type" "application/json"}
     :body (json-str (spec/explain-data ::subscription-detail (:body req)))}))

(def routes
  [(POST "/commands/subscribeToMachine" [] subscribe-to-machine)])
