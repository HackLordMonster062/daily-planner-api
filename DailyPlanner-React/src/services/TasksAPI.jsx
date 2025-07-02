import axios from 'axios';

const API_BASE_URL = 'https://localhost:7225';

const TaskAPI = {
    project: {
        create: async (projectData) => {
            try {
                const response = await axios.post(`${API_BASE_URL}/Project/AddProject`, projectData);
                return response.data;
            } catch (error) {
                console.error('Error creating project:', error);
                throw error;
            }
        },
    
        getAll: async () => {
            try {
                const response = await axios.get(`${API_BASE_URL}/Project`);
                return response.data;
            } catch (error) {
                console.error('Error fetching projects:', error);
                throw error;
            }
        },

        getByID: async (id) => {
            try {
                //const response = await axios.get(`${API_BASE_URL}/Project/${id}`)
                alert("Get by ID not yet implemented")
            } catch (error) {
                console.error('Error fetching projects:', error);
                throw error;
            }
        },
    
        update: async (id, updatedData) => {
            throw new NotImplementedError('Update project functionality is not implemented yet.');
            try {
                const response = await axios.put(`${API_BASE_URL}/${id}`, updatedData);
                return response.data;
            } catch (error) {
                console.error(`Error updating task with ID ${id}:`, error);
                throw error;
            }
        },
    
        delete: async (id) => {
            try {
                const response = await axios.delete(`${API_BASE_URL}/Project/RemoveProject/${id}`);
                return response.data;
            } catch (error) {
                console.error(`Error deleting task with ID ${id}:`, error);
                throw error;
            }
        },
    },
    
    timeline: {
        create: async (timelineData) => {
            try {
                const response = await axios.post(`${API_BASE_URL}/Timeline/AddTimeline`, timelineData);
                return response.data;
            } catch (error) {
                console.error('Error creating timeline:', error);
                throw error;
            }
        },

        delete: async (id) => {
            try {
                const response = await axios.delete(`${API_BASE_URL}/Timeline/RemoveTimeline/${id}`);
                return response.data;
            } catch (error) {
                console.error(`Error deleting timeline with ID ${id}:`, error);
                throw error;
            }
        },

        addItem: async (type, itemData) => {
            try {
                const response = await axios.post(`${API_BASE_URL}/Timeline/AddItem`, { type, ...itemData });
                return response.data;
            } catch (error) {
                console.error('Error adding item to timeline:', error);
                throw error;
            }
        },

        deleteItem: async (projectId, timelineId, itemId) => {
            try {
                const response = await axios.delete(`${API_BASE_URL}/Timeline/RemoveItem`, { data: { projectId, timelineId, itemId } });
                return response.data;
            } catch (error) {
                console.error(`Error deleting item in timeline ${timelineId} with ID ${itemId}:`, error);
                throw error;
            }
        },
    }
};

export default TaskAPI;