<template>
    <div class="modal" v-if="show">
        <div class="modal_backdrop" @click="closeModal()"/>
        <slot name="header"/>
        <table class="table">
          <thead>
            <tr>
              <th>User</th>
              <th>Value</th>
              <th>&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="rsvp in rsvps" :key="rsvp.User">
              <td>{{ rsvp.User }}</td>
              <td>{{ rsvp.Value }}</td>
              <td class="text-right">
                <a href="#" @click.prevent="updateRSVP(rsvp)">Edit</a> -
              </td>
            </tr>
          </tbody>
        </table>
        <title>{{ (model.id ? 'Edit RSVP' + model.id : 'New RSVP') }}</title>
            <form @submit.prevent="createRSVP">
                <div class="form-group">
                    <label>RSVP User</label>
                    <input type="text" v-model="model.User"><input>
                </div>

                <!-- Should use RSVP model Values created but I am being lazy... -->
                <div class="form-group">
                    <label>Value</label>
                    <div class="basic dropdown">
                        <select>
                            <option>No Response</option>
                            <option>Attending</option>
                            <option>Remotely Attending</option>
                            <option>Tentatively Attending</option>
                            <option>Not Attending</option>
                        </select>
                    </div>
                </div>
                <div>
                    <button type="submit" variant="success">Save RSVP</button>
                </div>
            </form>
    </div>
</template>

<script>
    import api from '@/services/PassageWaysTestAPIService';

    export default {
        data() {
            return {
                show: false,
                loading: false,
                rsvps: [],
                model: {}
            };
        },
        async created() {
            
        },
        methods: {
            async getRSVPs() {
                this.loading = true

                try {
                this.rsvps = await api.getByName(model.Name)
                } finally {
                this.loading = false
                }
            },
            async updateRSVP(rsvp) {
                this.model = Object.assign({}, rsvp);
            },
            async createRSVP() {
                const isUpdate = !!this.model.id;

                if (isUpdate) {
                await api.update(this.model.id, this.model)
                } else {
                await api.create(this.model)
                }

                this.model = {}

                await this.getRSVPs()
            },
            closeModal() {
                this.show = false;
            },
            openModal() {
                this.show = true;
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

            return this.rsvps.sort(compare);
            }
        }
    }
</script>