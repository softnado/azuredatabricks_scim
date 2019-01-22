# azuredatabricks_scim
Demo project about how to use Azure Databricks SCIM API

This project demo how to use Azure Databricks SCIM API to auto sync accounts between MS AD Security Group and Azure Databricks Workspace

Please check the App.config to necessary app settings first.

The demo console app require two Security Groups:

1. Security Group contains the members need stay sync with Azure Databricks;

2. Permission Group validate if the member in Security Group have necessary permission to access data;

The assumption is:

1. Org uses Permission Group to control who can access sensitive data;

2. Not all people who can access sensitive data need access Azure Databricks;

3. Org uses Security Group to control who can access Azure Databricks;

This demo uses stand .NET AD Framework instead of Azure AAD/Microsoft Graph due to complex auth requirement needed for Azure AAD/Microsoft Graph.

Please feel free to add comments or send questions.