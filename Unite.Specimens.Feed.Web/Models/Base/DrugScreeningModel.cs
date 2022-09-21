﻿using System.Text.Json;

namespace Unite.Specimens.Feed.Web.Models.Base;

public record DrugScreeningModel
{
    private string _drug;
    private double? _dss;
    private double? _dssSelective;
    private double? _gof;
    private double? _minConcentration;
    private double? _maxConcentration;
    private double? _absIC25;
    private double? _absIC50;
    private double? _absIC75;
    private double[] _concentration;
    private double[] _inhibition;
    private double[] _dose;
    private double[] _response;


    /// <summary>
    /// Drug name
    /// </summary>
    public string Drug { get => _drug?.Trim(); init => _drug = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric)
    /// </summary>
    public double? Dss { get => _dss; init => _dss = value; }

    /// <summary>
    /// Drug sensitivity score (asymmetric) selective
    /// </summary>
    public double? DssSelective { get => _dssSelective; init => _dssSelective = value; }

    /// <summary>
    /// Drug goodness of fit score
    /// </summary>
    public double? Gof { get => _gof; init => _gof = value; }

    /// <summary>
    /// Minimal tested concentration in nano molar
    /// </summary>
    public double? MinConcentration { get => _minConcentration; init => _minConcentration = value; }

    /// <summary>
    /// Maximal concentration tested in nano molar
    /// </summary>
    public double? MaxConcentration { get => _maxConcentration; init => _maxConcentration = value; }

    /// <summary>
    /// Average concentration at 25% inhibition
    /// </summary>
    public double? AbsIC25 { get => _absIC25; init => _absIC25 = value; }

    /// <summary>
    /// Average concentration at 50% inhibition
    /// </summary>
    public double? AbsIC50 { get => _absIC50; init => _absIC50 = value; }

    /// <summary>
    /// Average concentration at 75% inhibition
    /// </summary>
    public double? AbsIC75 { get => _absIC75; init => _absIC75 = value; }

    /// <summary>
    /// Concentration at corresponding inhibition percent
    /// </summary>
    public double[] Concentration { get => _concentration; set => _concentration = value; }

    /// <summary>
    /// Percent inhibition at corresponding concentration
    /// </summary>
    public double[] Inhibition { get => _inhibition; set => _inhibition = value; }

    /// <summary>
    /// Concentration at (N)th inhibition percent (for drug response curve)
    /// </summary>
    public double[] Dose { get => _dose; set => _dose = value; }

    /// <summary>
    /// Percent inhibition at (N)th concentration (for drug response curve)
    /// </summary>
    public double[] Response { get => _response; set => _response = value; }
}
