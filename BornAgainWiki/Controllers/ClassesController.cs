﻿using BornAgainWiki.Models.Classes;
using BornAgainWiki.Providers;
using Microsoft.AspNetCore.Mvc;
using Ronin.Model;
using Ronin.Model.Definitions;
using Ronin.Model.Enums;

namespace BornAgainWiki.Controllers;

public class ClassesController : Controller
{
	private readonly ObjectLibraryProvider _objectLibraryProvider;

	public ClassesController(ObjectLibraryProvider objectLibraryProvider)
	{
		_objectLibraryProvider = objectLibraryProvider;
	}

	public async Task<IActionResult> Index()
	{
		var library = await _objectLibraryProvider.GetLibraryAsync();
		if (library == null)
		{
			// TODO error
			return RedirectToAction("Error", "Home");
		}

		var model = new ClassesViewModel(library.All().OfType<CharacterDefinition>().Where(x => x.Flags.HasNone(ObjectFlags.Hide)));
		return View(model);
	}
}
