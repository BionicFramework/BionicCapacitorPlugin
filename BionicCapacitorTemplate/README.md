# Bionic Capacitor Template

Capacitor framework template for Bionic Projects

## Deployment Notes:

There are a few things to take into consideration when creating your Blazor WASM project for Capacitor deployment
1. Root page must include a second page route: @page("/index.html")
1. index.html must have a base haref of "./": &#60;base href="./" /&#62;
1. Android does not like directories that start with _
