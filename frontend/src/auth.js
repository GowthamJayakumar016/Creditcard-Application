const TOKEN_KEY = 'token';
const ROLE_KEY = 'role';
const USERNAME_KEY = 'username';

export function saveAuth(auth) {
  localStorage.setItem(TOKEN_KEY, auth.token || '');
  localStorage.setItem(ROLE_KEY, auth.role || '');
  localStorage.setItem(USERNAME_KEY, auth.userName || '');
}

export function clearAuth() {
  localStorage.removeItem(TOKEN_KEY);
  localStorage.removeItem(ROLE_KEY);
  localStorage.removeItem(USERNAME_KEY);
}

export function getToken() {
  return localStorage.getItem(TOKEN_KEY);
}

export function getRole() {
  return localStorage.getItem(ROLE_KEY);
}

export function getUserName() {
  return localStorage.getItem(USERNAME_KEY);
}

export function isAuthenticated() {
  return !!getToken();
}
