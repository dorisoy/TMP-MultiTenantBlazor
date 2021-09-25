netsh http add urlacl url=http://localhost:42973/ user=everyone
netsh http add urlacl url=http://tenant1.localhost:42973/ user=everyone
netsh http add urlacl url=http://tenant2.localhost:42973/ user=everyone
netsh http add urlacl url=http://tenant3.localhost:42973/ user=everyone

netsh http add urlacl url=https://localhost:44364/ user=everyone
netsh http add urlacl url=https://tenant1.localhost:44364/ user=everyone
netsh http add urlacl url=https://tenant2.localhost:44364/ user=everyone
netsh http add urlacl url=https://tenant3.localhost:44364/ user=everyone

