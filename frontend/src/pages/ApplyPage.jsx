import { useState } from 'react';
import { apiRequest } from '../api';

function ApplyPage() {
  const [form, setForm] = useState({
    name: '',
    panNumber: '',
    dateOfBirth: '',
    annualIncome: ''
  });
  const [message, setMessage] = useState('');
  const [error, setError] = useState('');

  function updateField(name, value) {
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setMessage('');
    setError('');

    if (!form.name.trim() || !form.panNumber.trim() || !form.dateOfBirth || !form.annualIncome) {
      setError('All fields are required.');
      return;
    }

    const payload = {
      name: form.name,
      panNumber: form.panNumber,
      dateOfBirth: form.dateOfBirth,
      annualIncome: Number(form.annualIncome)
    };

    const result = await apiRequest('/applications', {
      method: 'POST',
      body: JSON.stringify(payload)
    });

    if (!result.ok) {
      setError(result.message);
      return;
    }

    setMessage('Application submitted successfully.');
    setForm({ name: '', panNumber: '', dateOfBirth: '', annualIncome: '' });
  }

  return (
    <div className="container">
      <div className="row justify-content-center">
        <div className="col-md-8">
          <div className="card">
            <div className="card-header bg-primary text-white">Apply for Credit Card</div>
            <div className="card-body">
              {message && <div className="alert alert-success">{message}</div>}
              {error && <div className="alert alert-danger">{error}</div>}
              <form onSubmit={handleSubmit}>
                <div className="mb-3">
                  <label className="form-label">Name</label>
                  <input className="form-control" value={form.name} onChange={(e) => updateField('name', e.target.value)} />
                </div>
                <div className="mb-3">
                  <label className="form-label">PAN Number</label>
                  <input className="form-control" value={form.panNumber} onChange={(e) => updateField('panNumber', e.target.value)} />
                </div>
                <div className="mb-3">
                  <label className="form-label">Date of Birth</label>
                  <input type="date" className="form-control" value={form.dateOfBirth} onChange={(e) => updateField('dateOfBirth', e.target.value)} />
                </div>
                <div className="mb-3">
                  <label className="form-label">Annual Income</label>
                  <input type="number" className="form-control" value={form.annualIncome} onChange={(e) => updateField('annualIncome', e.target.value)} />
                </div>
                <button type="submit" className="btn btn-primary">Submit Application</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ApplyPage;
