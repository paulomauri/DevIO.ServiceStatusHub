using ServiceStatusHub.Domain.Entities;

public interface IIncidentHistoryRepository
{
    Task AddAsync(IncidentHistory history);
    Task<IncidentHistory> GetByIdAsync(Guid id);
    Task<List<IncidentHistory>> GetByIncidentIdAsync(Guid incidentId);
    Task<List<IncidentHistory>> GetRecentAsync(Guid incidentId, int count = 50);
    Task DeleteAsync(Guid id);
}

/*
 * 
Situação	            Action	            Description
Incidente criado	    "Created"	        "Sistema detectou falha 500"
Status alterado	        "StatusChanged"	    "De Critical para Warning"
Comentário adicionado	"CommentAdded"	    "Chamado aberto para equipe NOC"
Anexo incluído	        "AttachmentAdded"	"upload_log.txt"
Incidente resolvido	    "Resolved"	        "Aplicação reiniciada com sucesso"
*/