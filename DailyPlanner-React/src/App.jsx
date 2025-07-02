import { Suspense, useEffect, useState } from 'react'
import './App.css'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Dashboard from '@/pages/Dashboard';
import Projects from './pages/Projects';
import MainLayout from './layouts/MainLayout';
import ProjectEditor from './pages/ProjectEditor';

function App() {
    return (
        <>
            <Suspense fallback={null}>
                <BrowserRouter>
                    <Routes>
                        <Route element={<MainLayout />}>
                            <Route index path="dashboard" element={<Dashboard />} />
                            <Route path="projects" element={<Projects />} />
                            <Route path="projects/:projectId" element={<ProjectEditor />} />
                        </Route>
                    </Routes>
                </BrowserRouter>
            </Suspense>
        </>
    )
}

export default App
