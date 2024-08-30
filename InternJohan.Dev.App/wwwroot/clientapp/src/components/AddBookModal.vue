<template>
    <div class="modal fade show d-block" tabindex="-1" aria-labelledby="addBookModalLabel" aria-modal="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="addBookModalLabel">Add a New Book</h5>
                    <button type="button" class="btn-close" aria-label="Close" @click="closeModal"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="addBook">
                        <div class="mb-3">
                            <label for="title" class="form-label">Title</label>
                            <input v-model="title" id="title" type="text" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="author" class="form-label">Author</label>
                            <input v-model="author" id="author" type="text" class="form-control" required />
                        </div>
                        <div class="mb-3">
                            <label for="ISBN" class="form-label">ISBN</label>
                            <input v-model="isbn" id="ISBN" type="text" class="form-control" required />
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-primary">Add Book</button>
                            <button type="button" class="btn btn-secondary" @click="closeModal">Cancel</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from 'vue';

    export default defineComponent({
        name: 'AddBookModal',
        emits: ['add-book', 'close'],
        setup(props, { emit }) {
            const title = ref('');
            const author = ref('');
            const isbn = ref('');

            const closeModal = () => {
                emit('close');
            };

            const addBook = () => {
                emit('add-book', { title: title.value, author: author.value, ISBN: isbn.value }); // Ändra till 'ISBN' med stora bokstäver
                closeModal();
            };

            return {
                title,
                author,
                isbn,
                closeModal,
                addBook,
            };
        },
    });
</script>

<style scoped>
    .modal {
        display: block;
    }

    .modal-backdrop {
        display: none;
    }
</style>
