﻿using System.Text.Json.Serialization;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record CellLineInfoModel
{
    private string _name;
    private string _depositorName;
    private string _depositorEstablishment;
    private DateTime? _establishmentDate;

    private string _pubMedLink;
    private string _atccLink;
    private string _exPasyLink;


    /// <summary>
    /// Publicly known cell line name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get => _name?.Trim(); init => _name = value; }

    /// <summary>
    /// Depositor name
    /// </summary>
    [JsonPropertyName("depositor_name")]
    public string DepositorName { get => _depositorName?.Trim(); init => _depositorName = value; }

    /// <summary>
    /// Establishment, where the cell line was produced
    /// </summary>
    [JsonPropertyName("depositor_establishment")]
    public string DepositorEstablishment { get => _depositorEstablishment?.Trim(); init => _depositorEstablishment = value; }

    /// <summary>
    /// Date when the cell line was produced
    /// </summary>
    [JsonPropertyName("establishment_date")]
    public DateTime? EstablishmentDate { get => _establishmentDate; init => _establishmentDate = value; }


    /// <summary>
    /// Link to the cell line in PubMed
    /// </summary>
    [JsonPropertyName("pub_med_link")]
    public string PubMedLink { get => _pubMedLink?.Trim(); init => _pubMedLink = value; }

    /// <summary>
    /// Link to the cell line in ATCC
    /// </summary>
    [JsonPropertyName("atcc_link")]
    public string AtccLink { get => _atccLink?.Trim(); init => _atccLink = value; }

    /// <summary>
    /// Link to the cell line in ExPasy
    /// </summary>
    [JsonPropertyName("ex_pasy_link")]
    public string ExPasyLink { get => _exPasyLink?.Trim(); init => _exPasyLink = value; }
}
