pipeline {
    agent {
        kubernetes {
            yamlFile 'kubernetes/agent-dotnet.yaml'
        }
    }
    options{
        timeout(time: 30, unit: 'MINUTES')
        disableConcurrentBuilds()
        buildDiscarder(logRotator(numToKeepStr: '3'))
    }
    environment {
        KUBE_NAMESPACE = 'credit'
        HELM_RELEASE_NAME = 'collateral'
        DOCKER_IMAGE = 'RIPE'
        PROGET = 'http://proget.easynvest.com.br:81/endpoints/helmcharts/content/default-dotnet-app-0.1.3.tgz'
    }
    stages{
        stage("Build app dotnet"){
            steps {
                container('dotnet'){
                    sh "make build"
                }
            }
        }
        stage("Unit tests"){
            steps{
                container('dotnet'){
                    sh "make test-ci"
                }
            }
        }
        stage("Static Analysis"){
            steps{
                container('dotnet'){
                    withSonarQubeEnv('SonarServer'){
                        sh "make static analysis"
                    }
                }
            }
        }
        stage("Quality gate"){
            steps {
                container('dotnet'){
                    sh "sleep 10"
                    timeout(time: 5, unit: 'MINUTES'){
                        waitForQualityGate abortPipeline: true
                    }
                }
            }
        }
        stage("Build and Push Docker Image"){
            when {
                    anyOf {
                        branch 'develop'
                        branch 'master'
                    }
                }
            steps{
                container('dotnet'){
                    script{
            docker.withRegistry('https://827461279864.dkr.ecr.sa-east-1.amazonaws.com/RIPE', 'ecr:sa-east-1:svc_alm') {
                docker.build('${DOCKER_IMAGE}:${BUILD_ID}', '-f Dockerfile-jenkins .')
                docker.image('${DOCKER_IMAGE}:${BUILD_ID}').push()
                        }
                    }
                }
            }
        }
        stage("Deploy HOM") {
            when { branch 'develop'}
            environment {
                ENVIRONMENT = "Homolog"
                KUBE_CONTEXT = "k8s.hom.easynvest.io"
                VALUES_YAML = "kubernetes/homolog.yaml"
            }
            steps {
                container('dotnet') {
                sh "make deploy"
                    }
            }
        }
        stage("Deploy PROD") {
            when { branch 'master'}
            environment {
                ENVIRONMENT = "Production"
               KUBE_CONTEXT = "k8s.prod.easynvest.io"
                VALUES_YAML = "kubernetes/production.yaml"
            }
            steps {
                input (message: 'Prosseguir com deploy em Produção?', ok: 'Prosseguir')
                container('dotnet') {
                sh "make deploy"
                }
            }
        }
    }
}