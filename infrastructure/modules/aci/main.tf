resource "azurerm_container_group" "aci" {
  name = var.aci_name
  location = var.location
  resource_group_name = var.resource_group_name
  ip_address_type = "Public"
  dns_name_label = var.aci_name
  os_type   = "Linux"

  container {
    name = "hello-world"
    image = var.container_image
    cpu = "0.5"
    memory = "1.5"

    ports {
        port = 8080
        protocol = "TCP"
    }
  }
    # Add this to expose the port
  exposed_port {
    port     = 8080
    protocol = "TCP"
  }
}   