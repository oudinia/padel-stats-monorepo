# ./variables.tf
variable "resource_group_name" {
  default = "my-acr-rg"
}

variable "location" {
  default = "eastus"
}

variable "acr_name" {
  default = "myacrinstanceodtotal"
}

variable "aci_name" {
  default = "my-aci-instance-od-total"
}

variable "container_image" {
  default = "mcr.microsoft.com/azuredocs/aci-helloworld:latest"
}