[Requirements](Requirements.md)

# User Story #

As an Guest
I want to Logon
So that I can administer the site`

# Scenario 1 #

Given a valid User Name
> And a valid Password

When I logon

Then I expect to see the admin functions

# Scenario 2 #

Given an invalid User Name

When I logon

Then an invalid user message should be displayed

# Scenario 3 #

Given an invalid Password

When a user logs on

Then an invalid password message should be displayed