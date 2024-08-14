SELECT assunto, ano, count(id) quantidade
FROM atendimentos
GROUP BY ano, assunto
HAVING count(id) > 3
ORDER BY ano DESC, count(id) DESC;