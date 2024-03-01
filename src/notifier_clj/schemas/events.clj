(ns notifier-clj.schemas.events
  (:require [clojure.spec.alpha :as spec]))

(spec/def ::UserId string?)
(spec/def ::MachineId string?)
(spec/def ::user-subscribed-to-machine
  (spec/keys :req-un [::UserId ::MachineId]))

(defn make-event [type subject data]
  (let [event
        {:type type
         :time (java.time.Instant/now)
         :subject subject
         :data data}]
    (when-not (spec/valid? type data)
      (def debug-make-event
        {:event event :spec (spec/explain-data type data)}))
    event))
