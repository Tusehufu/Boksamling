<template>
    <div class="modal fade show d-block" tabindex="-1" aria-labelledby="bookDetailsModalLabel" aria-modal="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="bookDetailsModalLabel">{{ book.title }}</h5>
                    <button type="button" class="btn-close" aria-label="Close" @click="closeModal"></button>
                </div>
                <div class="modal-body">
                    <p><strong>Author:</strong> {{ book.author }}</p>
                    <p><strong>ISBN:</strong> {{ book.ISBN }}</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="closeModal">Close</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, PropType } from 'vue';

    export default defineComponent({
        name: 'BookDetailsModal',
        props: {
            book: {
                type: Object as PropType<{ title: string; author: string; ISBN: string }>,
                required: true
            }
        },
        emits: ['close'],
        setup(props, { emit }) {
            const closeModal = () => {
                emit('close');
            };

            return {
                closeModal,
            };
        }
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
