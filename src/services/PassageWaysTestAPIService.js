import Vue from 'vue';
import axios from 'axios';

const client = axios.create({
    baseURL: 'htttp://localhost:5000/api/passagewaystest',
    json: true
});

export default {
    async execute(method, resource, data) {
        const accessToken = await Vue.prototype.$auth.getAccessToken()
        return client({
            method,
            url: resource,
            data,
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        }).then(req => {
            return req.data
        })
    },
    getAll() {
        return this.execute('get', '/')
    },
    getByName(name) {
        return this.execute('get', `/${name}`)
    },
    create(data) {
        return this.execute('post', '/', data)
    },
    update(name, data) {
        return this.execute('put', `/${type}`, data)
    },
}
