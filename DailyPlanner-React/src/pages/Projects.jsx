import { useEffect, useState } from "react";
import TaskAPI from "@/services/TasksAPI";

export default function Projects() {
    const [projects, setProjects] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchProjects = async () => {
            try {
                const data = await TaskAPI.project.getAll();
                setProjects(data);
            } catch (err) {
                setError(err.message || 'Failed to load projects');
            }
        };

        fetchProjects();
    }, []);

    return (
        <>
            <div className="p-4">
                <h1 className="text-2xl font-bold mb-4">Project List</h1>
                {error && <p className="text-red-500">Error: {error}</p>}
                {projects.length === 0 ? (
                    <p>Loading...</p>
                ) : (
                    <ul className="space-y-2">
                    {projects.map((project) => (
                        <li key={project.id} className="p-2 border rounded shadow">
                        <h2 className="text-lg font-semibold">{project.title}</h2>
                        <p>{project.description}</p>
                        </li>
                    ))}
                    </ul>
                )}
            </div>
        </>
    )
}