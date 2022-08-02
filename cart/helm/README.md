# Installing Chart

´ helm install cart-chart cart-service -f cart-service/values.yaml´

# Upgrading Chart

´ helm upgrade cart-chart cart-service -f cart-service/values.yaml´

# Forward port 

 kubectl port-forward cart-chart-cart-service-<pod> 3000:3000

# TODO: Migrations are not yet being executed, so for
# testing purposes, do a kubectl get pods and exec into the 
# pod to run the database setup script for now
