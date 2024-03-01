(ns notifier-clj.projections.statistics)

(defonce statistics (atom {}))

(defn project [_ _ old-event-store new-event-store]
  (doseq [event (drop (count old-event-store) new-event-store)]
    (swap! statistics update (:type event) #(inc (or % 0)))))
