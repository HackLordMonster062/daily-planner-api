import TaskAPI from "@/services/TasksAPI";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

export default function () {
    const { projectId } = useParams();
    const [project, setProject] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchProject = async () => {
            try {
                const data = await TaskAPI.project.getByID(projectId);
                setProject(data);
            } catch (err) {
                setError(err.message || 'Failed to load project');
            }
        };

        fetchProject();
    }, [projectId]);

    return (
        <>
        </>
    )
}