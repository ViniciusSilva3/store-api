# Installing Chart

´ helm install payment-chart payment-service -f payment-service/values.yaml´

# Upgrading Chart

´ helm upgrade payment-chart payment-service -f payment-service/values.yaml´

# Forward port 

 kubectl port-forward payment-chart-payment-service-<pod> 3000:3000

# TODO: Migrations are not yet being executed, so for
# testing purposes, do a kubectl get pods and exec into the 
# pod to run the database setup script for now