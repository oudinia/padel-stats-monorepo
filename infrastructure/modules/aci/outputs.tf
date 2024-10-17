output "aci_fqdn" {
  value = azurerm_container_group.aci.fqdn
  description = "The FQDN of the Azure Container Instance"
}

output "aci_ip_address" {
  value = azurerm_container_group.aci.ip_address
  description = "The public IP address of the Azure Container Instance"
}

output "aci_port" {
    value = 8080
    description = "The port exposed by the Azure Container Instance"
}