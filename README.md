# RIPE
[![Coverage](http://sonarqube.k8s.easynvest.io/api/project_badges/measure?project=RIPE&metric=coverage)](http://sonarqube.k8s.easynvest.io/dashboard?id=RIPE)[![Maintainability Rating](http://sonarqube.k8s.easynvest.io/api/project_badges/measure?project=RIPE&metric=sqale_rating)](http://sonarqube.k8s.easynvest.io/dashboard?id=RIPE)[![Quality Gate Status](http://sonarqube.k8s.easynvest.io/api/project_badges/measure?project=RIPE&metric=alert_status)](http://sonarqube.k8s.easynvest.io/dashboard?id=Easynvest.MGM)

Objetivo: Evidenciar os investimentos elegíveis como forma de garantia para a adesão dos serviços disponíveis.

## Produção
* [Kibana](http://kibana.easynvest.com.br/_plugin/kibana/app/kibana#/discover?_g=()&_a=(columns:!(_source),interval:auto,query:(language:kuery,query:'Assembly%20:%20RIPE.API'),sort:!('@timestamp',desc)))
* [Health Check](http://apis-internal.easynvest.io/Collateral/health)

## Homologação
* [Kibana](http://kibana-homolog.easynvest.com.br/_plugin/kibana/app/kibana#/discover?_g=()&_a=(columns:!(_source),interval:auto,query:(language:kuery,query:'Assembly%20:%20RIPE.API'),sort:!('@timestamp',desc)))
* [Swagger](http://apis-internal.hom.easynvest.io/Collateral/swagger/index.html)
* [Health Check](http://apis-internal.hom.easynvest.io/Collateral/health)
* [Jenkins](http://jenkins.k8s.easynvest.io/job/Credit/job/RIPE/)