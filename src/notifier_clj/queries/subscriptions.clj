(ns notifier-clj.queries.subscriptions
  (:require [compojure.core :refer [GET]]
            [clojure.data.json :refer [json-str]]
            [notifier-clj.projections.statistics :as stat]))

(defn- statistics [_]
  {:status 200
   :headers {"Content-Type" "application/json"}
   :body (json-str @stat/statistics)})

(def routes
  [(GET "/queries/Statistics" [] statistics)])
