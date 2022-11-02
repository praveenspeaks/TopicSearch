# TopicSearch
Learning .NET core API with elastic search deployment on docker and Kubernetes
Orchestration of multiple services from one deployment with network bridge

# Prerequisite
This application is just a learning to integrate elastic search with .net Core 6.0 and building an API to store Topic Data and User Data. You need to have docker or podmon installed in your computer as linux containers.

# AWS commands
aws configure - to configure your account 

check configuration 
  aws sts get-caller-identity
  
  if there is error then hack to fix the configuration issue
  
  unset AWS_SECRET_ACCESS_KEY
  
  unset AWS_SECRET_KEY

  export AWS_ACCESS_KEY_ID=[access-key-aws-account]
  
  export AWS_SECRET_ACCESS_KEY=[secret-aws-account]


# Docker Commands

There is 2 docker compose file created to run this application, first is to run with the lready created image and pushed to docker hub
docker-compose up -da

Note : to build image use : docker build -t [repoName]:[tagname] .

To run this application directly from the code to generate fresh image, rename "docker-compose-with_build.yml" file run below command
docker-compose up -d --build

once this is deployed on docker 
we can run API : http://localhost:49162
Elastic Search : http://localhost:49161

# Creating cluster onto aws account
to run below command first you have to install eksctl application from https://community.chocolatey.org/packages/eksctl, it will install kubectl as well

command to create new cluster with nodegroup and ec2 instance
eksctl create cluster --name [Cluster-Name] --region [RegionName] --nodegroup-name [NodeName: anyname you like] --node-type t3.micro --nodes 2

## update kube config to run kube commands
aws eks --region [region-name] update-kubeconfig --name [cluster-name]

check c:/users/[username]/.kube/config to verify the configuration, if all ok then run
eksctl get cluster -- will show list of clusters on your aws account

# Kubes commands
to create kube deployment file below command will generate yaml content which can be copied to yaml file for deployment

kubectl create deployment topic-search --image aryajasdev/topicsearch:v1 --dry-run=client --output yaml
kubectl create deployment elasticsearch --image docker.elastic.co/elasticsearch/elasticsearch:8.2.0  --dry-run=client --output yaml

## expose a deployment (defaults to type cluster ip)
kubectl expose deployment topic-search --port 80 --target-port 80 --type Loadbalancer --dry-run client --output yaml
kubectl expose deployment elasticsearch --port 9200 --target-port 9200 --type ClusterIp --dry-run client --output yaml

# Deploy
once above cluster will be created on your AWS Account run below command to depoy your containers to K8s
kubectl apply -f deployment.yaml

## Bash to access POD
kubectl exec -it [pod-name] -- bash

## Check the status of Pods
kubectl get pods

## check the servics
kubectl get svc
this will give you load balancer and dns to check the url

## check the deployments
kubectl get deployments

## delete the deployment
kubectl delete deployment deployment-name

## check everything
kubectl get all

## check pod configuration
kubectl describe [pod-name]
