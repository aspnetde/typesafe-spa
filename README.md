# Typesafe SPA

This is a small demo that explores different approaches for creating a potentially scalable single page application.

## Structure

![Structure](./structure.png "Structure")

## Functionality

- There is a global session object that potentially can hold shared state for all parts of the application.
- The login page authenticates the user and initates the creation of the session.
- After logging in, the user is redirected to the dashboard, where they are welcomed by name.
- There is an additional profile page, showing the name again.
- From the profile page it's possible to go to an "edit profile" page that allows changing that name.
- After the name has changed, that change must be reflected in all parts of the application: dashboard, and profile page.
- Although "edit profile" could be seen as a child of "profile", it is kept on the same level for simplicity reasons (we potentially already are at the third level here).


## Goals

That scope of functionality allows us to look at the following questions:

1. How are parent-child relationships handled?
2. How much boiler-plate code needs to be written?
3. How much guidance and support provides the compiler?
4. And after all: How maintanable aka scalable does the approach appear to be?

## Approaches

1. [Elmish](https://elmish.github.io/elmish/) + [Feliz](https://github.com/Zaid-Ajaj/Feliz)
