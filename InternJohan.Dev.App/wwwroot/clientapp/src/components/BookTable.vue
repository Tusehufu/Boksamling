<template>
  <div>
    <button class="btn btn-primary mb-3" @click="openAddBookModal">Add Book</button>
    <table class="table table-bordered">
      <thead class="thead-light">
        <tr>
          <th>Title</th>
          <th>Author</th>
          <th>ISBN</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="book in books" :key="book.ISBN" @click="selectBook(book)">
          <td>{{ book.title }}</td>
          <td>{{ book.author }}</td>
          <td>{{ book.ISBN }}</td>
        </tr>
      </tbody>
    </table>

    <AddBookModal v-if="isAddBookModalOpen" @add-book="addBook" @close="isAddBookModalOpen = false" />
    <BookDetailsModal v-if="selectedBook" :book="selectedBook" @close="selectedBook = null" />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import AddBookModal from './AddBookModal.vue';
import BookDetailsModal from './BookDetailsModal.vue';

export default defineComponent({
  name: 'BookTable',
  components: { AddBookModal, BookDetailsModal },
  setup() {
    const books = ref([
      { title: 'En bok', author: 'En författare', ISBN: '151515' },
      { title: 'En annan bok', author: 'En annan författare', ISBN: '121212' },
      { title: 'En till bok', author: 'En till författare', ISBN: '141414' },
      { title: 'Sista boken', author: 'En sista författare', ISBN: '131313' }
    ]);

    const isAddBookModalOpen = ref(false);
    const selectedBook = ref<{ title: string; author: string; ISBN: string } | null>(null);

    const openAddBookModal = () => {
      isAddBookModalOpen.value = true;
    };

    const addBook = (book: { title: string; author: string; ISBN: string }) => {
      books.value.push(book); 
      isAddBookModalOpen.value = false;
    };

    const selectBook = (book: { title: string; author: string; ISBN: string }) => {
      selectedBook.value = book;
    };

    return {
      books,
      isAddBookModalOpen,
      selectedBook,
      openAddBookModal,
      addBook,
      selectBook,
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

.table tbody tr {
  cursor: pointer;
}

.table tbody tr:hover {
  background-color: #f1f1f1;
}
</style>
