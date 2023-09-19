workspace {

    model {
        user = person "User"
        admin = person "Admin"
        softwareSystemGraphExplorer = softwareSystem "Microsoft Graph Explorer"

        softwareSystemMadWordSuite = softwareSystem "MadWorld Suite" {
            frontendAdmin = container "Frontend Admin" {
                admin -> this "Uses"
            }

            frontendSuite = container "Frontend Suite" {
                user -> this "Uses"
            }

            apiAnonymous = container "API Anonymous" {
                frontendSuite -> this "Uses"
                frontendAdmin -> this "Uses"
            }

            apiAuthorized = container "API Authorized" {
                frontendAdmin -> this "Uses"
                this -> softwareSystemGraphExplorer "Gets & Modify Azure AD accounts"
            }

            jobRunner = container "JobRunner"

            storageAccount = container "Storage Account" {
                tags "StorageAccount"
                
                blobsStorage = component "Blobs Storage" {
                    apiAnonymous -> this "Uses"
                    apiAuthorized -> this "Uses"
                    jobRunner -> this "Get blogs & Removes expired blogs"
                }

                tableStorage = component "Table Storage" {
                    apiAnonymous -> this "Uses"
                    apiAuthorized -> this "Uses"
                    jobRunner -> this "Removes expired blogs"
                }
            }
        }
    }

    views {
        systemContext softwareSystemMadWordSuite "SystemMadWorldSuite" {
            include *
            autoLayout
        }

        container softwareSystemMadWordSuite "ContainerMadWorldSuite" {
            include *
            autoLayout
        }

        component storageAccount "ComponentStorageAccount" {
            include *
        }

        styles {
            element "StorageAccount" {
                shape Cylinder
            }
        }

        theme default
    }

}