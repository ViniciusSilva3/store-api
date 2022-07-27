kube apply -f payment-pv.yaml
kube apply -f payment-pv-claim.yaml

helm install postgresql-dev -f values.yaml bitnami/postgresql --set volumePermissions.enabled=true