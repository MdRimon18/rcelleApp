"use strict";

/* -------------------------------------------------------------------------- */
/*                              Config                                        */
/* -------------------------------------------------------------------------- */
window.loadScript = function (scriptUrl) {
    return new Promise((resolve, reject) => {
        const scriptElement = document.createElement('script');
        scriptElement.src = scriptUrl;
        scriptElement.onload = resolve;
        scriptElement.onerror = reject;
        document.body.appendChild(scriptElement);
    });
};



var CONFIG = {
  isNavbarVerticalCollapsed: false,
  theme: 'light',
  isRTL: false,
  isFluid: false,
  navbarStyle: 'transparent',
  navbarPosition: 'vertical'
};
Object.keys(CONFIG).forEach(function (key) {
  if (localStorage.getItem(key) === null) {
    localStorage.setItem(key, CONFIG[key]);
  }
});
if (JSON.parse(localStorage.getItem('isNavbarVerticalCollapsed'))) {
  document.documentElement.classList.add('navbar-vertical-collapsed');
}
if (localStorage.getItem('theme') === 'dark') {
  document.documentElement.setAttribute('data-bs-theme', 'dark');
} else if (localStorage.getItem('theme') === 'auto') {
  document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
}
//# sourceMappingURL=config.js.map
