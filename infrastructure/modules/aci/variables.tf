variable "aci_name" {
  description = "Name of the Azure Container Instance"
  type = string
}

variable "location" {
  description = "Azure region where the ACI will be created"
  type = string
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type = string
}

variable "container_image" {
  description = "Docker image to deploy"
  type = string
  default = "mcr.microsoft.com/azuredocs/aci-helloworld:latest"
}