(defproject notifier-clj "0.1.0-SNAPSHOT"
  :description "FIXME: write description"
  :url "http://example.com/FIXME"
  :license {:name "EPL-2.0 OR GPL-2.0-or-later WITH Classpath-exception-2.0"
            :url "https://www.eclipse.org/legal/epl-2.0/"}
  :dependencies [[org.clojure/clojure "1.11.1"]
                 [org.clojure/data.json "2.5.0"]
                 [http-kit "2.3.0"]
                 [compojure "1.6.1"]
                 [ring/ring-json "0.5.1" :exclusions [ring/ring-codec]]
                 [ring/ring-core "1.11.0" :exclusions [ring/ring-codec commons-codec]]]
  :main ^:skip-aot notifier-clj.core
  :target-path "target/%s"
  :profiles {:uberjar {:aot :all
                       :jvm-opts ["-Dclojure.compiler.direct-linking=true"]}})
