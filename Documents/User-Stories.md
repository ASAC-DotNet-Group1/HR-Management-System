## Story One

1. Title: Login Page
2. User Story: sentence as a user I want to have a sign up / login page to that I can save my own data
3. Feature Tasks:

- create a login page using authentication and authorization.

4. Acceptance Tests:

- Trying to login using real accounts, the result should be accepting the account.
- Trying to login using fake accounts, the result should be rejecting the account.

## Story Two

1. Title: Addind a new employee page
2. User Story: As an admin, you can add a new employee to the system, it will ask to add an ID, full name, age, phone number, email, password, department and level
3. Feature Tasks:

- Create an new page that allows admins to add a new employee to the database

4. Acceptance Tests:

- Adding a new employee with a unique email and username will create a new employee successfully.
- Adding a new employee with a duplicated email and username will send an error message.

## Story Three

1. Title: Search for an employee page.
2. User Story: when an admin is signed in, he will have the option to search for an employee by their ID and view all of their personal information.
3. Feature Tasks:

- Search for an employee page.

- 4. Acceptance Tests:

- Searching for an employee with an existant ID will search and view all of their personal information.
- Searching for an employee with a non-existant ID will send an error message.

## Story Four

1. Title: Delete Employee Page.
2. User Story: As an admin, you will have the option to delete an employee along with their data from the database
3. Feature Tasks:

- Delete an employee from the database.

- 4. Acceptance Tests:

- Deleting for an employee with an existant ID will delete the user and all of their personal information.
- Deleting for an employee with a non-existant ID will send an error message.

