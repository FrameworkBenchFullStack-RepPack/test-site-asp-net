// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("filter-form");
  if (!form) return;

  form.addEventListener("htmx:afterRequest", () => {
    const url = new URL(window.location.href);
    url.search = new URLSearchParams(new FormData(form)).toString();
    history.replaceState({}, "", url);
  });
});
