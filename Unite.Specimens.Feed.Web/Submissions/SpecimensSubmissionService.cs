using Unite.Cache.Configuration.Options;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Drugs;

namespace Unite.Specimens.Feed.Web.Submissions;

public class SpecimensSubmissionService
{
    private readonly Repositories.DrugsSubmissionRepository _drugsSubmissionRepository;
    private readonly Repositories.IntervensionsSubmissionRepository _intervensionsSubmissionRepository;
    private readonly Repositories.LinesSubmissionRepository _linesSubmissionRepository;
    private readonly Repositories.MaterialsSubmissionRepository _materialsSubmissionRepository;
    private readonly Repositories.OrganoidsSubmissionRepository _organoidsSubmissionRepository;
    private readonly Repositories.XenograftsSubmissionRepository _xenograftsSubmissionRepository;

    public SpecimensSubmissionService(IMongoOptions options)
	{
        _drugsSubmissionRepository = new Repositories.DrugsSubmissionRepository(options);
        _intervensionsSubmissionRepository = new Repositories.IntervensionsSubmissionRepository(options);
        _linesSubmissionRepository = new Repositories.LinesSubmissionRepository(options);
        _materialsSubmissionRepository = new Repositories.MaterialsSubmissionRepository(options);
        _organoidsSubmissionRepository = new Repositories.OrganoidsSubmissionRepository(options);
        _xenograftsSubmissionRepository = new Repositories.XenograftsSubmissionRepository(options);
	}

    public string AddDrugsSubmission(AnalysisModel<DrugScreeningModel> data)
	{
		return _drugsSubmissionRepository.Add(data);
	}

    public AnalysisModel<DrugScreeningModel> FindDrugsSubmission(string id)
	{
		return _drugsSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteDrugsSubmission(string id)
    {
        _drugsSubmissionRepository.Delete(id);
    }
    
    public string AddIntervensionsSubmission(Models.Specimens.InterventionsModel[] data)
	{
		return _intervensionsSubmissionRepository.Add(data);
	}

    public Models.Specimens.InterventionsModel[] FindIntervensionsSubmission(string id)
	{
		return _intervensionsSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteIntervensionsSubmission(string id)
    {
        _intervensionsSubmissionRepository.Delete(id);
    }

    public string AddLinesSubmission(Models.Specimens.LineModel[] data)
	{
	 	return _linesSubmissionRepository.Add(data);
	}

    public Models.Specimens.LineModel[] FindLinesSubmission(string id)
	{
		return _linesSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteLinesSubmission(string id)
    {
        _linesSubmissionRepository.Delete(id);
    }

    public string AddMaterialsSubmission(Models.Specimens.MaterialModel[] data)
	{
		return _materialsSubmissionRepository.Add(data);
	}

    public Models.Specimens.MaterialModel[] FindMaterialsSubmission(string id)
	{
		return _materialsSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteMaterialsSubmission(string id)
    {
        _materialsSubmissionRepository.Delete(id);
    }

    public string AddOrganoidsSubmission(Models.Specimens.OrganoidModel[] data)
	{
		return _organoidsSubmissionRepository.Add(data);
	}

    public Models.Specimens.OrganoidModel[] FindOrganoidsSubmission(string id)
	{
		return _organoidsSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteOrganoidsSubmission(string id)
    {
        _organoidsSubmissionRepository.Delete(id);
    }
    
     public string AddXenograftsSubmission(Models.Specimens.XenograftModel[] data)
	{
		return _xenograftsSubmissionRepository.Add(data);
	}

    public Models.Specimens.XenograftModel[] FindXenograftsSubmission(string id)
	{
		return _xenograftsSubmissionRepository.Find(id)?.Document;
	}

    public void DeleteXenograftsSubmission(string id)
    {
        _xenograftsSubmissionRepository.Delete(id);
    }
}