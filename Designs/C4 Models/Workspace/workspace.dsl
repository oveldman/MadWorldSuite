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
                apiAuthorized -> this "Uses"
                apiAnonymous -> this "Uses"
                jobRunner -> this "Uses"
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

        styles {
            element "StorageAccount" {
                shape Cylinder
            }
        }

        theme default
    }

}