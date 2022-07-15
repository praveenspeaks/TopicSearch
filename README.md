# TopicSearch
Learning .NET core API with elastic search deployment on docker and Kubernetes
Orchestration of multiple services from one deployment with network bridge


#Prerequisite

to play with this you must have docker installed with kubernates

to run the appliccation
docker-compose up -d --build


right now both services working sepratly but when deploying togather api is not accessible on port exposed


# Kubes commands

## create (have manually added ports but can be added to command)


kubectl create deployment topic-search --image aryajasdev/topicsearch:v1 --dry-run=client --output yaml
kubectl create deployment elasticsearch --image docker.elastic.co/elasticsearch/elasticsearch:8.2.0  --dry-run=client --output yaml

## expose a deployment (defaults to type cluster ip)


kubectl expose deployment topic-search --port 80 --target-port 80 --type Loadbalancer --dry-run client --output yaml
kubectl expose deployment elasticsearch --port 9200 --target-port 9200 --type ClusterIp --dry-run client --output yaml

## Deploy
kubectl apply -f deployment.yaml

## Bash

kubectl exec -it [pod-name] -- bash
