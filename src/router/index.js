// Basic Vue Imports
import Vue from "vue";
import VueRouter from "vue-router";

// Okta imports
import Auth from '@okta/okta-vue';

// Personal imports
import MeetingDashboard from '@/components/MeetingDashboard';

Vue.use(Auth, {
  issuer: 'https://bimetics.okta.com/oauth2/default',
  client_id: '0oad7xrhaTu3JFTVN5d6',
  redirect_ui: 'https://localhost:5001/implicit/callback',
  scope: 'openid profile email'
});

Vue.use(VueRouter);

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'MeetingDashboard',
      component: MeetingDashboard,
      props: true
    },
    {
      path: '/implicit/callback',
      component: Auth.handleCallback()
    },
  ]
});

router.beforeEach(Vue.prototype.$auth.authRedirectGuard());

export default router;