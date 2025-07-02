import { NavLink, Outlet } from "react-router-dom";

export default function MainLayout() {
    return (
        <>
            <div className="w-full min-h-fit h-1/12 bg-gray-800
                            flex flex-row justify-center"
            >
                <NavLink
                    to="/dashboard" 
                    className={({ isActive }) => "nav-link " + (isActive ? 'active' : '')}
                >
                    Dashboard
                </NavLink>
                <NavLink 
                    to="/projects" 
                    className={({ isActive }) => "nav-link " + (isActive ? 'active' : '')}
                >
                    Projects
                </NavLink>
            </div>

            <Outlet />
        </>
    )
}