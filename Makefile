SHELL := /bin/sh

build:
	dotnet publish -c Release -o app --configfile NuGet.config 

test-ci:
	dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude="[*.IoC*]*%2c[*.Data*]*%2c[*xunit*]*"

static analysis:
	dotnet sonarscanner begin /k:"RIPE"  /d:sonar.cs.opencover.reportsPaths="test/*/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/*.Ioc*/**/*.*,**/*.Data*/**/*.*,**Test*.cs" && dotnet build --configfile NuGet.config && dotnet sonarscanner end

deploy:
	kubectl config use-context ${KUBE_CONTEXT} && \
    helm upgrade --install --namespace=${KUBE_NAMESPACE} ${HELM_RELEASE_NAME} --values=${VALUES_YAML} \
    --set-string image.tag=${BUILD_ID} --set environment=${ENVIRONMENT} \
    ${PROGET} --force