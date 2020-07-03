# Type-Safe SPA

This is a small demo that explores different approaches for creating a potentially scalable single page application. See [here](https://aspnetde.github.io/typesafe-spa/) for the completely unspectacular sample apps in action.

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
2. How much boilerplate code needs to be written?
3. How much guidance and support provides the compiler?
4. How maintainable, aka scalable, does the approach appear to be?

## Approaches

### Elmish + Feliz

- Based on [Fable](https://fable.io/), [Elmish](https://elmish.github.io/elmish/), and [Feliz](https://github.com/Zaid-Ajaj/Feliz)
- [Demo app](https://aspnetde.github.io/typesafe-spa/elmish-feliz)

#### Observations

- When introducing intents (aka external messages), the parent doesn't know about them until we start handling them there. So the compiler wouldn't complain if we forget to add it to the parent(s).
- Routing can fail silently when the expected route isn't found. Sometimes that's just a matter of lower and upper case letters.
- `Feliz.Router` is best being used with arrays representing the url segments, because that's what can be passed to the `format()` function (it does not accept lists).
- The Feliz syntax felt rather verbose at first. Unfortunately, `Html` is a type and not a module and therefore "cannot be opened". However, in direct comparison to the "classic" Elmish view syntax, for real world tasks there seems not to be that much of a difference in terms of the number of written lines of code. Good old HTML will in most cases still stay shorter, though...
- Except for commands fully testable, as everything is (mostly pure) F# functions
- Does not require much knowledge of React

### React Function Components + Feliz

- Based on [Fable](https://fable.io/), [Feliz](https://github.com/Zaid-Ajaj/Feliz), and [Felize useElmish](https://zaid-ajaj.github.io/Feliz/#/Feliz/UseWithElmish)
- [Demo app](https://aspnetde.github.io/typesafe-spa/function-components-feliz)

#### Observations

- Much less boilerplate code to write in comparison to Elmish + Feliz
- It is basically React written with F#, so React knowledge is needed
- In addition, regular updates are needed whenever React changes
- Working with the context API of react needs getting used to, but is then relatively straightforward
- Passing data to components through props is straightforward as well
- `useElmish` is useful for more complex forms (see login and edit profile), but simple operations can also be handled more pragmatically (see logout)
- Right now, Hot Module Reloading (or Fast Refresh) [does not work](https://github.com/Zaid-Ajaj/Feliz/issues/203). Which is a clear obstacle during development time.
- TBD: Testability

### React + TypeScript

- Based purely on [React](https://reactjs.org/), and [TypeScript](https://www.typescriptlang.org/)
- [Demo app](https://aspnetde.github.io/typesafe-spa/react-typescript/)

#### Observations

- Having not actively worked with TypeScript since its early days in 2012, it was surprisingly easy to get started.
- Although this small demo doesn't use much of the language's features, those that are being used were really easy to pick up and a pleasure to work with.
- The tooling based on Visual Studio Code is amazing. I not only like the [automatic code-formatting](https://prettier.io/) but also the really fast feedback loop that is provided during development time.
- When the build breaks, it does so within milliseconds, and error messages are always helpful. Most of the time, however, it already becomes obvious that something went wrong while typing.
- I could take over all the concepts (components, context, ...) from the previous implementation with Reaction Function Components + Feliz, which allowed me to implement the TypeScript demo within a couple of hours.
- I decided to implement the two parts of the app which contain a bit more "logic" (login + edit user name) without following any strict pattern like MVU. As everything is self-contained in its own component, this seems reasonable.
- react-router is powerful but relatively easy to get started with.
- As with the Feliz sample, function components are easy to reason about.

## Comparison

### Lines of Code

Although it is not the most important factor, it might be interesting to see how much more or less code every approach produces. This comparison was created through [cloc](https://github.com/AlDanial/cloc) and only counts those files that are actually used to write the app. So the compiled JavaScript, for example, is not considered.

| Approach                    | App Files | Blanks | Comments | Code |
| --------------------------- | --------- | ------ | -------- | ---- |
| Function Components + Feliz | 13        | 59     | 6        | 316  |
| Elmish + Feliz              | 11        | 77     | 6        | 404  |
| React + TypeScript          | 11        | 27     | 1        | 241  |

#### Performance and bundle size

The following is measured by looking at the [final and production-ready compile SPAs](https://aspnetde.github.io/typesafe-spa/) with Google Chrome (83) on macOS. It is obviously not objective and may be improved by some settings (I just used the default configuration as provided by the templates I used). But it gives an idea about what to expect.

| Approach                    | Requests | Transferred | Resources | DOMContentLoaded | Load   |
| --------------------------- | -------- | ----------- | --------- | ---------------- | ------ |
| Function Components + Feliz | 8        | 253 KB      | 466 KB    | 167 ms           | 166 ms |
| Elmish + Feliz              | 7        | 253 KB      | 468 KB    | 171 ms           | 173 ms |
| React + TypeScript          | 7        | 216 KB      | 328 KB    | 131 ms           | 132 ms |

(Transferred = compressed, Resources = uncompressed; in each case the fastest run was chosen)

## Resources

- [Elmish Parent-child composition](https://elmish.github.io/elmish/parent-child.html#Parent-child-composition)
- [Elm Shared State example](https://github.com/ohanhi/elm-shared-state)
- [Design of Large Elm apps](https://groups.google.com/forum/#!msg/elm-discuss/_cfOu88oCx4/madaA1rBAQAJ)
- [elm-taco-donut](https://github.com/madasebrof/elm-taco-donut)
- [Pros/cons of Elmish vs plain React components (via Fable.React)](https://github.com/elmish/elmish/issues/154)
- [Child-Parent Communication in Elm: OutMsg vs Translator vs NoMap Patterns](https://medium.com/@_rchaves_/child-parent-communication-in-elm-outmsg-vs-translator-vs-nomap-patterns-f51b2a25ecb1)
- [TypeScript docs](https://www.typescriptlang.org/docs/home.html)
- [React Router](https://reacttraining.com/react-router/web/example/basic)
- [React+TypeScript Cheatsheets](https://github.com/typescript-cheatsheets/react-typescript-cheatsheet#reacttypescript-cheatsheets)
- [Create React App](https://create-react-app.dev/)
