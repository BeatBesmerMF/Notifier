(ns notifier-clj.events.event-store)

(defonce store (atom []))

(defn store-events [events]
  (swap! store concat events))
