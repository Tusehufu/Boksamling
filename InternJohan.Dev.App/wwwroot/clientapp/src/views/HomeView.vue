<template>
    <div class="home">
        <h1 class="text-center" display="1">Välkommen till boksamlingen</h1>
        <BookTable></BookTable>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
    import BookTable from '../components/BookTable.vue';
export default defineComponent({
  name: 'HomeView',
    components: {
      BookTable,
  },
});
</script>
