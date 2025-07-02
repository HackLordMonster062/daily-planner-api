export default function TaskItem({
    title,
    description,
    dueDate,
    isCompleted,
    focus,
    priority
}) {
    return (
        <div className={`min-h-fit w-1/3 p-3 rounded bg-[${isCompleted ? "green-300" : "gray-400"}]`}>
            <h1>{title}</h1>
            <span>{description}</span>
        </div>
    )
}