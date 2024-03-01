(ns notifier-clj.core
  (:gen-class)
  (:require [org.httpkit.server :as server]
            [compojure.core :refer [defroutes routes]]
            [compojure.route :as route]
            [ring.middleware.json :refer [wrap-json-body]]
            [notifier-clj.commands.subscriptions :as C-subscriptions]
            [notifier-clj.queries.subscriptions :as Q-subscriptions]
            [notifier-clj.events.event-store :as event-store]
            [notifier-clj.projections.statistics :as statistics]))

(defroutes app-routes
  (apply routes C-subscriptions/routes)
  (apply routes Q-subscriptions/routes)
  (route/not-found "You Must Be New Here"))

(remove-watch event-store/store :project-store-to-statistics)
(add-watch event-store/store :project-store-to-statistics statistics/project)

(defn -main
  "This is our app's entry point"
  [& _]
  (let [port   (Integer/parseInt (or (System/getenv "PORT") "8080"))]
    (server/run-server
     (-> app-routes
         (wrap-json-body {:keywords? true :bigdecimals? true}))
     {:port port})
    (println (str "Running webserver at http:/127.0.0.1:" port "/"))))

(comment
  (-main)
  )
