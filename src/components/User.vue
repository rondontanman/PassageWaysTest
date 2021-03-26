<template>
    <div class="username">{{ username }}</div>
</template>

<script>
export default {
    props: 
    { 
        username: String,
        email: String 
    },
}
</script>

<style lang="scss">
.username {
    color: blue;
    display: flex;
    align-items: center;
    justify-content: center;
}

.username:before {
    content: "'";
}

.username::after {
    content: "'";
}
</style>
