<template>
  <div>
    <button class="btn btn-primary mb-3" @click="openModal">Add Book</button>
    <table class="table table-bordered">
      <thead class="thead-light">
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>ISBN</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in books" :key="book.ISBN">
          <td>{{ book.title }}</td>
          <td>{{ book.author }}</td>
          <td>{{ book.ISBN }}</td>
        </tr>
      </tbody>
    </table>
    <AddBookModal v-if="isModalOpen" @add-book="addBook" @close="isModalOpen = false" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import AddBookModal from './AddBookModal.vue';

export default defineComponent({
  name: 'BookTable',
  components: { AddBookModal },
  setup() {
    const books = ref([
      { title: 'Great Expectations', author: 'Charles Dickens', ISBN: '151515' },
      { title: 'The Catcher in the Rye', author: 'J.D. Salinger', ISBN: '121212' },
      { title: 'To Kill a Mockingbird', author: 'Harper Lee', ISBN: '141414' },
      { title: '1984', author: 'George Orwell', ISBN: '131313' }
    ]);

    const isModalOpen = ref(false);

    const openModal = () => {
      isModalOpen.value = true;
    };

    const addBook = (book: { title: string, author: string, ISBN: string }) => {
      books.value.push({ title: book.title, author: book.author, ISBN: book.ISBN }); 
    };

    return {
      books,
      isModalOpen,
      openModal,
      addBook,
    };
  }
});
</script>

<style scoped>
.table {
  width: 100%;
  margin: 20px 0;
}

.thead-light th {
  background-color: #f4f4f4;
  font-weight: bold;
}
</style>
