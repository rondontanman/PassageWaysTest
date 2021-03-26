<template>
  <div id="app">
    <div id="nav">
      <nav href="#" @click.prevent="login" v-if="!user">Login</nav>
      <nav href="#" @click.prevent="logout" v-else>Logout</nav>
      <user v-if="user" :user="user.email"></user>
    </div>
    <main>
      <router-view :user="user"></router-view>
    </main>
  </div>
</template>

<script>
import User from "@/components/User";

export default {
  name: 'app',
  components: { User },
  data() {
    return {
      user: null
    }
  },
  async created() {
    await this.refreshUser()
  },
  watch: {
    '$route': 'onRouteChange'
  },
  methods: {
    login() {
      this.$auth.loginRedirect()
    },
    async onRouteChange() {
      await this.refreshUser()
    },
    async refreshUser() {
      this.user = await this.$auth.getUser()
    },
    async logout() {
      await this.$auth.logout()
      await this.refreshUser()
      this.$router.push('/')
    }
  }
}
</script>


<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
