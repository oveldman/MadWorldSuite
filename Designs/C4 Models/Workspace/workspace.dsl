workspace {

    model {
        user = person "User"
        admin = person "Admin"
        softwareSystemMadWordSuite = softwareSystem "MadWorld Suite"
        softwareSystemGraphExplorer = softwareSystem "Microsoft Graph Explorer"

        user -> softwareSystemMadWordSuite "Uses"
        admin -> softwareSystemMadWordSuite "Uses"
        softwareSystemMadWordSuite -> softwareSystemGraphExplorer "Gets & Modify Azure AD accounts"
    }

    views {
        systemContext softwareSystemMadWordSuite "MadWorldSuite" {
            include *
            autoLayout
        }

        theme default
    }

}