<template>
  <div id="dashboard">
    <h1 class="h1">Meeting Dashboard</h1>
    <span :show="error.length === 0 && !isLogged" variant="warning">Please login to be able to view meetings</span>
    <span :show="error.length > 0" variant="danger">{{ error }}</span>
    <span :show="loading" variant="info">Loading...</span>
    <div class="main">
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>User</th>
            <th>Date</th>
            <th>Start Time</th>
            <th>Length</th>
            <th>RSVPs</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="meeting in meetings" :key="meeting.name">
              <td>{{ meeting.Name }}</td>
              <td>{{ meeting.User }}</td>
              <td>{{ meeting.Date }}</td>
              <td>{{ meeting.StartTime }}</td>
              <td>{{ meeting.Length }}</td>
              <td>{{ meeting.RSVPs[0] + '...' || 'N/A'}}</td>
              <td class="edit">
                  <a href="#" @click.prevent="$refs.modalName.openModal()">View</a>
              </td>
          </tr>
        </tbody>
      </table>

      <rsvp-modal ref="modalName">
        <template v-slot:header>
          <h1>{{ meeting.Name }}</h1>
        </template>
      <rsvp-modal>
    </div>
  </div>
</template>

<style lang="scss">
.main {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>

<script>
import api from './services/PassageWaysTestAPIService.js';
import Modal from './modals/RSVPModal';

export default {
  components: {
    Modal
  },
  props: { user: Object },

  watch: {
    user: async function(newVal) {
      if (newVal) {
        this.loggedInUser = newVal;
        this.userRecords = await this.getByName(newVal.name);
      }
    }
  },
  data() {
    return {
      loading: false,
      userRecords: {},
      error: "",
      isLogged: false,
      loggedInUser: this.user,
      meetings: []
    }
  },
  async created() {
    this.meetings = await api.getAll()
  },
  methods: {
    async getByName(name) {
      this.loading = true
      
      try {
        this.isLogged = true
        return await api.getByName(name)
      } catch(error) {
        if (error && error.response && error.response.status === 401) {
          this.isLogged = false
        }
        else {
          this.error = error.message
        }
      } finally {
        this.loading = false
      }
    }
  },
  computed: {
    sortedArray: function() {
      function compare(a, b) {
        if (a.Name < b.Name)
          return -1;
        if (a.Name > b.Name)
          return 0;
      }

      return this.meetings.sort(compare);
    }
  }
}
