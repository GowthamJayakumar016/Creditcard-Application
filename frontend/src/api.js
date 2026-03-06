import { getToken, clearAuth } from './auth';

const API_BASE_URL = 'http://localhost:5227/api';

export async function apiRequest(path, options = {}) {
  const token = getToken();
  const headers = {
    'Content-Type': 'application/json',
    ...(options.headers || {})
  };

  if (token) {
    headers.Authorization = `Bearer ${token}`;
  }

  try {
    const response = await fetch(`${API_BASE_URL}${path}`, {
      ...options,
      headers
    });

    let data = null;
    try {
      data = await response.json();
    } catch {
      data = null;
    }

    if (response.status === 401) {
      clearAuth();
      return { ok: false, status: response.status, message: 'Unauthorized. Please login again.', data };
    }

    if (!response.ok) {
      return {
        ok: false,
        status: response.status,
        message: data?.message || 'Request failed.',
        data
      };
    }

    return { ok: true, status: response.status, data, message: '' };
  } catch {
    return { ok: false, status: 0, message: 'Network error. Please check your connection.', data: null };
  }
}
